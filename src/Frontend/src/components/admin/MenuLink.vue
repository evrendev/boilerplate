<script>
export default {
  name: "MenuLink",
  props: {
    childs: {
      type: Array,
      default: () => []
    },
    label: {
      type: String,
      default: null
    }
  },
  computed: {
    routeName() {
      return this.$route.name
    }
  }
}
</script>

<template lang="pug">
  div
    li.sidebar-header
      | {{ label }}
    li.sidebar-item(v-for="child in childs"
      :class="{ 'active' : routeName == child.name }")
      router-link.sidebar-link(:to="child.to")
        <i :class="child.icon"></i>
        span.align-middle {{ child.label }}
</template>

<style lang="scss" scoped></style>
