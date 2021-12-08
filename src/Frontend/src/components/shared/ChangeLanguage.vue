<script>
import { mapState } from "vuex"
export default {
  name: "ChangeLanguage",
  computed: {
    ...mapState(["languages"])
  },
  methods: {},
  data: function () {
    return {
      lang: String
    }
  },
  mounted() {
    this.lang = this.$route.params.lang
  }
}
</script>

<template lang="pug">
  .language
    a.change-language(href="#") EN
</template>

<style lang="postcss" scoped></style>
