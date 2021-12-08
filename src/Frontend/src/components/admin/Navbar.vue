<script>
import ProfileMenu from "@/components/admin/ProfileMenu"
import LanguageMenu from "@/components/admin/LanguageMenu"
import MobileMenu from "@/components/admin/MobileMenu"

export default {
  name: "Navbar",
  components: {
    ProfileMenu,
    LanguageMenu,
    MobileMenu
  }
}
</script>

<template lang="pug">
nav.navbar.navbar-expand.navbar-light.navbar-bg
  mobile-menu
  .navbar-collapse.collapse
    ul.navbar-nav.navbar-align
      language-menu
      profile-menu
</template>
