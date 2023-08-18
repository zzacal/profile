<script>
  import Highlight from "../elements/Highlight.svelte";
</script>

<Highlight
  title="tilda"
  icon={{url: "/tilda.svg", alt: "blog hosted on github"}}
  description="a web server that mocks responses built with node and expressjs"
  links={[{
    url: "https://hub.docker.com/r/jizacal/tilda",
    icon: {
      url: "https://d36jcksde1wxzq.cloudfront.net/54e48877dab8df8f92cd.png",
      alt: "docker hub page"
    }
  },{
    url: "https://github.com/zzacal/tilda",
    icon: {
      url: "/github-inverse.svg",
      alt: "tilda source code"
    }
  }]} />
