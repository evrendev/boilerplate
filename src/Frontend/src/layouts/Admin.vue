<template lang="pug">
  .wrapper
    link(rel="preconnect" href="https://fonts.gstatic.com")
    link(href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600&display=swap" rel="stylesheet")
    sidebar-component
    .main
      navbar-component
      slot
      footer-component
</template>

<script>
import "bootstrap"
import "@fortawesome/fontawesome-free/css/all.min.css"
import "@fortawesome/fontawesome-free/js/all.js"

import sidebarComponent from "@/components/admin/Sidebar"
import navbarComponent from "@/components/admin/Navbar"
import footerComponent from "@/components/admin/Footer"

export default {
  name: "AdminLayout",
  components: {
    sidebarComponent,
    navbarComponent,
    footerComponent
  }
}
</script>

<style lang="scss" src="@/styles/admin/app.scss"></style>
