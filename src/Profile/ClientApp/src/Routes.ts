import Blog from "./lib/blog/Blog.svelte";
import Cv from "./lib/cv/Cv.svelte";

export const routes = [
  {
    name: "/",
    component: Cv
  },
  {
    name:"/blog/:year/:month/:day/:folder",
    component: Blog
  }
]
