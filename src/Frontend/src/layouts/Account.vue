<template lang="pug">
main.d-flex.w-100
  .container.d-flex.flex-column
    .row.vh-100
      slot
</template>

<script>
export default {
  name: "AccountLayout"
}
</script>

<style lang="postcss" scoped>
main {
  background-image: url("https://source.unsplash.com/daily?grayscale&blur");
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
