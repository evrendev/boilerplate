<script>
import { mapActions } from "vuex"

export default {
  name: "MobileMenu",
  methods: {
    ...mapActions("dashboard", ["toggleMenu"])
  }
}
</script>

<template lang="pug">
a.sidebar-toggle.d-flex(@click="toggleMenu")
  i.hamburger.align-self-center
</template>
