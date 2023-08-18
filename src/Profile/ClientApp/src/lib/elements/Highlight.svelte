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
    <img src={icon.url} alt={icon.alt}>
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

<style>
  .highlight {
    margin-top: 1rem;
    background-color: #134;
    padding: .5rem;
    border-radius: 1rem;
    overflow: hidden;
  }
  .links {
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
