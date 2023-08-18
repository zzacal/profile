<script>
  import Highlight from '../elements/Highlight.svelte';
import Disgpt from './Disgpt.svelte';
  import Pfp from './Pfp.svelte';
  import RollyRetry from './RollyRetry.svelte';
</script>

<header id="intro">
  <Pfp />
  <div id="summary">
    <div id="name">Zac</div>
    <h1>Software Engineer</h1>
    <h2>Seattle, WA</h2>
    <p>I make, build, and release software. <a href="https://www.linkedin.com/in/zac-zacal">See my work history</a>.</p>
  </div>
</header>

<h2 class="project-heading">Projects</h2>
<div class="project-list">
  <Disgpt />
  <RollyRetry />
</div>
<style lang="scss">
  .project-heading {
    text-decoration: underline;
  }

  .project-list {
    display: flex;
    flex: 1 1 0px;
    gap: 1rem;
    > :global(*) {
      flex: 1 1 0px;
    }
  }
  
  #intro {
    margin-top: 2rem;
    display: flex;
    align-items: center;
    gap: 1rem;
  }
  #summary {
    text-align: left;
  }
  #summary p {
    margin: 1rem 0 0;
  }
  #name, #summary h1, #summary h2 {
    font-size: 1rem;
    line-height: 1rem;
    margin: 0;
  }
</style>