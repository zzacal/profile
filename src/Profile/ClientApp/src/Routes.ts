import Blog from "./lib/blog/Blog.svelte";

export const routes = [
  {
    name: "/",
    component: Blog
  },
  {
    name:"/blog/:year/:month/:day/:folder",
    component: Blog
  }
]
