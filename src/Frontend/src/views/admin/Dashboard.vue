<template lang="pug">
main.content
  .container-fluid.p-0
    h1.h3-mb-3 
      strong Kontrol
      |  Paneli
    .row
      .col-12
        card
</template>

<script>
import card from "@/components/admin/Card"
export default {
  name: "Dashboard",
  components: {
    card
  }
}
</script>

<style lang="postcss" scoped></style>
