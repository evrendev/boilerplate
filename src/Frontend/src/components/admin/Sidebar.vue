<script>
import { mapState } from "vuex"
import menuLink from "./MenuLink.vue"
import items from "@/data/menu.js"

export default {
  name: "Sidebar",
  components: {
    menuLink
  },
  computed: {
    ...mapState("dashboard", ["menuOpened"])
  },
  data() {
    return {
      appName: process.env.VUE_APP_NAME,
      menuItems: items
    }
  }
}
</script>

<template lang="pug">
  nav#sidebar.sidebar(:class="[ menuOpened ? 'collapsed' : '' ]")
    .sidebar-content.js-simplebar
      a.sidebar-brand(href="#")
        span.align-middle {{ appName }} Admin
      ul.sidebar-nav
        menu-link(v-for="menuItem in menuItems"
          :label="menuItem.label"
          :childs="menuItem.childs")
</template>
