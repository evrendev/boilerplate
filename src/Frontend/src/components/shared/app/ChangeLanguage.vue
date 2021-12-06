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

<style lang="postcss" scoped>
.language {
  display: block;
  width: 30px;
  height: 50px;
  margin-right: var(--default-space);
  display: flex;
  align-items: center;
  justify-content: center;

  a {
    color: rgb(var(--white));
    font-size: 1.25em;
    margin: 0 var(--default-space);
  }
}
</style>
