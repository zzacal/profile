<script lang="ts">
  export let title: string;
  export let icon: Image;
  export let description: string;
  export let links: Link[];
  
  type Link = {
    url: string,
    icon: Image
  };

  type Image = {
    url: string;
    alt: string;
  };

</script>
<div class="highlight">
  {#if icon != null}
    <img class="icon" src={icon.url} alt={icon.alt}>
  {/if}
  <h2>{title}</h2>
  <p>{description}</p>

  <ul class="links">
    {#each links as link}
      <li>
        <a href={link.url}>
          <img class="link-icon" src={link.icon.url} alt={link.icon.alt}/>
        </a>
      </li>
    {/each}
  </ul>
</div>

<style lang="scss">
  @import "../../colors";
  .highlight {
    margin-top: 1rem;
    color: $text-light;
    background-color: $background-light;
    padding: .5rem;
    border-radius: 1rem;
    overflow: hidden;
    @media (prefers-color-scheme: light) {
      color: $text-dark;
      background-color: $background-dark;
    }
  }
  .icon {
    height: 3rem;
    width: 3rem;
    border-radius: 1.5rem;
  }
  .links {
    padding: 0;
    list-style: none;
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 1rem;
  }
  
  .link-icon {
    width: 2rem;
  }
</style>
