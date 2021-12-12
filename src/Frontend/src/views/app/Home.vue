<script>
import LangDropdown from "@/components/app/LangDropdown.vue"

export default {
  name: "Home",
  components: {
    LangDropdown
  }
}
</script>

<template lang="pug">
  .content
    <lang-dropdown />
    h1 {{ $t("message") }}
    <p class="date">Today's Date: {{ $d(new Date(), 'long') }}</p>
</template>
