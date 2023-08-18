<script>
  import Highlight from "../elements/Highlight.svelte";
</script>

<Highlight
  title="developer blog"
  icon={{url: "/github-inverse.svg", alt: "blog hosted on github"}}
  description="a blog built with svelte with data stored in github"
  links={[{
    url: "/blog",
    icon: {
      url: "/svelte.svg",
      alt:"a blog built with svelte"
    }
  }, {
    url: "https://github.com/zzacal/blog",
    icon: {
      url: "/github-inverse.svg",
      alt: "blog data store in github"
    }
  }]} />
