<script>
import { mapState } from "vuex"
export default {
  name: "Logo",
  props: {
    text: String
  },
  computed: {
    ...mapState(["mainTitle"])
  }
}
</script>

<template lang="pug">
  .logo
    a.logo(href="/" 
      :alt="mainTitle" 
      :title="mainTitle")
      img.logo.md(:alt="mainTitle" 
        src="@/assets/admin/img/logo.png")
</template>

<style lang="postcss" scoped>
.logo {
  z-index: 2;

  img {
    &.logo {
      height: 52px;

      &.md {
        display: none;
      }

      @media (--sm) {
        &.xs {
          display: none;
        }

        &.md {
          display: block;
        }

        height: 125px;
      }
    }
  }
}
</style>
