<script lang="ts">
  import {onMount} from "svelte";
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
    const response = await fetch("/api/blog");
    return await response.json();
  }
  onMount(async () => {
    page = await getBlogs();
  })
</script>
<div>
<h1>{page.blog.title}</h1>
{page.blog.body}
</div>
{#each page.list as meta}
	{meta.name} | {meta.date} | {meta.sha} | {meta.url}
{/each}
