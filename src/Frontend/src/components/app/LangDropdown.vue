<script>
import { mapActions, mapState } from "vuex"
import { languages } from "@/locales/i18n"

export default {
  name: "langDropdown",
  data() {
    return {
      languageArray: languages
    }
  },
  methods: {
    ...mapActions("app", ["changeLocale"])
  },
  computed: {
    ...mapState("app", ["locale"]),
    lang: {
      get: function () {
        return this.locale
      },
      set: function (loc) {
        this.changeLocale(loc)
      }
    }
  }
}
</script>

<template lang="pug">
.lang-dropdown
  select(v-model="lang")
    option(v-for="(lang, i) in languageArray" 
      :key="`lang${i}`" 
      :value="lang")
      | {{ lang }}
</template>
