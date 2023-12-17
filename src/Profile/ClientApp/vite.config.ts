import { defineConfig } from "vite";
import { svelte } from "@sveltejs/vite-plugin-svelte";
import { VitePWA } from "vite-plugin-pwa";
import basicSsl from "@vitejs/plugin-basic-ssl";

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 5177,
    proxy: {
      "/api": {
        target: "https://localhost:7230",
        changeOrigin: true,
        secure: false,
      },
    },
  },
  plugins: [
    svelte(),
    basicSsl(),
    VitePWA({
      workbox: {
        globPatterns: ["**/*.{js,css,html,svg,png,woff2}"],
      },
      registerType: "autoUpdate",
      manifest: {
        background_color: "#2B2B2B",
        theme_color: "#e3d4bf",
        name: "thezacal",
        short_name: "thezacal",
        start_url: "/",
        display: "standalone",
        icons: [
          {
            src: "/chi.png",
            sizes: "715x715",
            type: "image/png",
            purpose: "maskable any",
          },
        ],
      },
      devOptions: {
        enabled: true
      },
    }),
  ],
});
