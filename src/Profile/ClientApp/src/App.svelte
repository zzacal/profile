<script lang="ts">
  import { routes } from './Routes';
  import Navigation from './lib/Navigation.svelte';
  import { Router } from "svelte-router-spa";
</script>
<Navigation />
<main>
    <Router {routes}/>
</main>

<style>
</style>
