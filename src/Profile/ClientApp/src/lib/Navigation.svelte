<script lang="ts">
  import linkedin from '../assets/linkedin.svg';
  import github from '../assets/github.svg';
</script>
  
<nav>
  <ul>
    <li><a href="/">blog</a></li>
    <li><a href="https://www.linkedin.com/in/zac-zacal" target="_blank"><img class="link-svg" src={linkedin} alt="LinkedIn" /></a></li>
    <li><a href="https://github.com/zzacal" target="_blank"><img class="link-svg" src={github} alt="Github" /></a></li>
  </ul>
</nav>

<style>
  ul {
    list-style: none;
    margin: 0;
    padding: 0;
  }
  li {
    display: inline-block;
  }
  li + li::before {
    content: " | ";
  }
  
  .link-svg {
    width: 1.6rem;
    position: relative;
    top: .5rem;
  }
</style>
