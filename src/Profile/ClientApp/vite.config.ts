import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    proxy: {
      '/weatherforecast': 'http://localhost:5101'
    }
  },
  plugins: [svelte()],
})
