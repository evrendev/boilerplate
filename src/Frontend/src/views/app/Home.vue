<script>
import LangDropdown from "@/components/app/LangDropdown.vue"

export default {
  name: "Home",
  components: {
    LangDropdown
  },
  metaInfo: {
    title: "Evren.Dev",
    titleTemplate: "%s - Web Application",
    htmlAttrs: {
      lang: "tr",
      amp: false
    }
  }
}
</script>

<template lang="pug">
  .content
    <lang-dropdown />
    h1 {{ $t("message") }}
    <p class="date">Today's Date: {{ $d(new Date(), 'long') }}</p>
</template>
