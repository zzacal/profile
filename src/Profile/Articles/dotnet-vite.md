# dotnet and vite to use other frameworks
So you love dotnet but you want to work with front end frameworks other than react or angular.
This article explains how to use vite with dotnet to achieve just that.

You can see the sample project [here](https://github.com/zzacal/dotnet-svelte).

## what you'll learn
1. Create a new dotnet project
1. Pick a front-end framework using [vite](https://vitejs.dev/)
1. Create a release build
1. Set up the development environment

## what you'll need
1. Some experience with dotnet's react/angular templates or a good reason to try this.
1. [The dotnet 7 sdk](https://dotnet.microsoft.com/en-us/download). (This should also work with dotnet 6)
1. [nodejs 18](https://nodejs.org/en/download)

## create a new dotnet react project and remove react
Open your terminal of choice, navigate to your repos folder and create a new dotnet react project.

```sh
dotnet new react -o WeatherApp
```

You can start the application using `dotnet run`.
```sh
cd WeatherApp
dotnet run
```
The dotnet application is now running and listening at `https://localhost:7006`. Your address might be different, but you'll need it way later when you [configure a proxy](#configure-a-proxy) 

Now you can delete all the react contents of this app. Don't worry, the react community will be fine if you don't use it this time.

```sh
rm WeatherApp/ClientApp
```

Your client app folder should be empty now.

![no react](https://raw.githubusercontent.com/zzacal/profile/main/src/Profile/Articles/dotnet-vite/no-react.png)

## use vite to pick your front end framework
We're going to follow the [vite Getting Started guide](https://vitejs.dev/guide/) to create a svelte application.

In the WeatherApp/ folder, kick off create-vite.

Commands:
```sh
cd WeatherApp
npm create vite@latest
```

Go through the prompts naming the project ClientApp and select Svelte with TypeScript. This creates a new Weather/ClientApp folder.

Output:
```sh
Need to install the following packages:
  create-vite@4.4.0
Ok to proceed? (y) y
√ Project name: ... ClientApp
√ Package name: ... clientapp
√ Select a framework: » Svelte
√ Select a variant: » TypeScript

Scaffolding project in C:\Users\Jizac\repos\dotnet-svelte\WeatherApp\ClientApp...

Done. Now run:

  cd ClientApp
  npm install
  npm run dev
```

Like it says, run the app!

Commands:
```sh
cd weather-app
npm install
npm run dev
```

Output:
```sh
  VITE v4.4.4  ready in 3269 ms

  ➜  Local:   http://localhost:5173/
  ➜  Network: use --host to expose
  ➜  press h to show help
```

Vite is now running at `http://localhost:5173/`.

![vite+svelte webpage](https://raw.githubusercontent.com/zzacal/profile/main/src/Profile/Articles/dotnet-vite/vite-svelte-default.png)

# use dotnet to create a release build
Open the `WeatherApp/WeatherApp.csproj` file and locate the `<DistFiles>` node inside of the `<Target Name="PublishRunWebpack">` node.

Current:
```xml
<DistFiles Include="$(SpaRoot)build\**" />
```

By default vite builds the project into the `/dist` folder. You need to update this node accordingly.

Updated:
```xml
<DistFiles Include="$(SpaRoot)dist\**" />
```

Now you can publish your project and see the front end files under wwwroot. Go to the WeatherApp folder and run the `publish` command. The output should show the dotnet build and npm build.

Commands:
```sh
cd WeatherApp
dotnet publish
```

Output:
```sh
MSBuild version 17.6.8+c70978d4d for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  WeatherApp -> C:\repos\dotnet-svelte\WeatherApp\bin\Debug\net7.0\WeatherApp.dll
  
  up to date, audited 97 packages in 221ms

  10 packages are looking for funding
    run `npm fund` for details

  > clientapp@0.0.0 build
  > vite build

  vite v4.4.4 building for production...
  transforming...
  ✓ 31 modules transformed.
  rendering chunks...
  computing gzip size...
  dist/index.html                  0.46 kB │ gzip: 0.30
   kB
  dist/assets/svelte-a39f39b7.svg  1.95 kB │ gzip: 0.91
   kB
  dist/assets/index-9ea02431.css   1.30 kB │ gzip: 0.65
   kB
  dist/assets/index-63969859.js    5.96 kB │ gzip: 2.74
   kB
  ✓ built in 276ms
  WeatherApp -> C:\repos\dotnet-svelte\WeatherApp\bin\Debug\net7.0\publish\
```
![dotnet-publish results](https://raw.githubusercontent.com/zzacal/profile/main/src/Profile/Articles/dotnet-vite/dotnet-publish-result.png)

Congratulations! At this point, if you published this output, you have a working svelte site served by dotnet.

## use vite to create a development environment
We are ready to set up our development environment. We need to accomplish some things to get a good developer experience:
1. Configure https on vite so we don't have to deal with non-tls calls
1. Configure a proxy for `/api` so we can forward calls to the backend
1. Tell dotnet where vite is serving the client app

### configure https
Navigate to the WeatherApp/ClientApp folder and install `@vitejs/plugin-basic-ssl` as a devDependency. You should see a message regarding the package being added after running the command.

Commands:
```sh
cd WeatherApp/Client
npm i -D @vitejs/plugin-basic-ssl
```

Open `vite.config.ts` and add the basic-ssl plugin. 
```typescript
import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import basicSsl from '@vitejs/plugin-basic-ssl'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [svelte(), basicSsl()],
})
```

### configure a proxy
Open the `vite.config.ts` and add the proxy configuration. This sample config will forward requests to vite at `http://localhost:5173/api/*` to dotnet debug at `https://localhost:7006/api/*`. 

```typescript
import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import basicSsl from '@vitejs/plugin-basic-ssl'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7006', 
        changeOrigin: true
      }
    }
  },
  plugins: [svelte(), basicSsl()],
})
```

Now you need to make sure your backend controllers are pre-fixed with `/api`. For example, `[Route("api/[controller]")]`.
Grouping all backend calls under `/api` is for convenience. There are [other options](https://vitejs.dev/config/server-options.html#server-proxy) you can use if you want to pick a different strategy. 

### connect dotnet to the vite server
To tell dotnet where vite is being served, open the `WeatherApp/WeatherApp.csproj` file and update the Spa configurations in the following nodes.

Before:
```xml
    <SpaProxyServerUrl>https://localhost:44461</SpaProxyServerUrl>
    <SpaProxyLaunchCommand>npm start</SpaProxyLaunchCommand>
```

After:
```xml
    <SpaProxyServerUrl>https://localhost:5173</SpaProxyServerUrl>
    <SpaProxyLaunchCommand>npm run dev</SpaProxyLaunchCommand>
```

Now you can start the dotnet application and it will automatically start the vite server!

Commands:
```sh
cd WeatherApp
dotnet run
```

Output:
```sh
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7006
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5247
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\repos\dotnet-svelte\WeatherApp
```

Navigate to `https://localhost:7006/` and you should be automatically directed to `https://localhost:5173/`

![vite+svelte webpage](https://raw.githubusercontent.com/zzacal/profile/main/src/Profile/Articles/dotnet-vite/vite-svelte-default.png)
