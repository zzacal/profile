<script lang="ts">
  import {onMount} from "svelte";
  import showdown from "showdown";
  export let currentRoute;
  let {year, month, day, folder} = currentRoute.namedParams;
  const path = [year, month, day, folder].filter(i => !!i).join('/');
  type BlogPage = {
    blog: Blog,
    list: Array<BlogMeta>
  };
  type Blog = {
    title: string,
    body: string
  };
  type BlogMeta = {
    sha: string,
    path: string,
    url: string,
    name: string,
    date: string
  };

  let count: number = 0
  let page: BlogPage = {
    blog: {
      title: "",
      body: ""
    },
    list: []
  };

  const getBlogs = async () : Promise<BlogPage> => {
    count += 1;
    const response = await fetch(`/api/blog?path=${path}`);
    return await response.json();
  };

  onMount(async () => {
    const converter = new showdown.Converter(page.blog.body);
    page = await getBlogs();
    page.blog.body = converter.makeHtml(page.blog.body);
  });
</script>
<h1>{path}</h1>
<div id="blog" class='blog-body'>
  {@html page.blog.body}
</div>
{#each page.list as meta}
	{meta.name} | {meta.date} | {meta.sha} | {meta.url}
{/each}

<style>
.blog-body {
  text-align: left;
}
</style>