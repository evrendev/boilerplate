<script>
import { mapState } from "vuex"
export default {
  name: "Title",
  computed: {
    ...mapState(["mainTitle"])
  }
}
</script>

<template lang="pug">
    .site-title {{ mainTitle }} 
</template>

<style lang="postcss" scoped>
.site-title {
  @media (--sm) {
    width: 50%;
  }

  margin-left: var(--default-space);
  color: rgba(var(--grey), 0.75);
}
</style>
