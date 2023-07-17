import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import basicSsl from '@vitejs/plugin-basic-ssl'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 5177,
    proxy: {
      '/api': {
        target: 'https://localhost:7230', 
        changeOrigin: true,
        secure: false,
      }
    }
  },
  plugins: [svelte(), basicSsl()],
})
