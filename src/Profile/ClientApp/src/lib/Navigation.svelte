<nav>
  <ul>    
    <li><a href="/">home</a></li>
    <li><a href="/blog">blog</a></li>
    <li><a href="https://www.linkedin.com/in/zac-zacal" target="_blank"><img class="link-svg" src="/linkedin.svg" alt="LinkedIn" /></a></li>
    <li><a href="https://github.com/zzacal" target="_blank"><img class="link-svg" src="/github.svg" alt="Github" /></a></li>
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
    content: " • ";
    font-size: 1.5rem;
    vertical-align: sub;
  }
  
  .link-svg {
    width: 1.6rem;
    position: relative;
    top: .5rem;
  }
</style>
