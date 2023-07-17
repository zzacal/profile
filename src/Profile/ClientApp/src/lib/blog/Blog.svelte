<script lang="ts">
  import {onMount} from "svelte";
  type BlogMeta = {
    path: string,
    url: string
  }

  let count: number = 0
  let blogs: Array<BlogMeta> = [];
  const getBlogs = async () : Promise<Array<BlogMeta>> => {
    count += 1;
    const response = await fetch("/api/blog");
    return await response.json();
  }
  onMount(async () => {
    blogs = await getBlogs();
  })
</script>

{#each blogs as blog}
	{blog.path} | {blog.url}
{/each}
