<script>
  import Highlight from "../elements/Highlight.svelte";
</script>

<Highlight
  title="sophon (aka disgpt)"
  icon={{url: "/disgpt.png", alt: "disgpt persona named sophon"}}
  description="A discord chat-gpt bot that will answer your questions"
  links={[{
    url: "https://discord.com/api/oauth2/authorize?client_id=1089437295456829600&permissions=274877975552&scope=bot%20applications.commands",
    icon: {
      url: "/discord-inverse.svg",
      alt:"invite on discord"
    }
  }, {
    url: "https://github.com/zzacal/disgpt",
    icon: {
      url: "/github-inverse.svg",
      alt:"disgpt source code"
    }
  }]} />
