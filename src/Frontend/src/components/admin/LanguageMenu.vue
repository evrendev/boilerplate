<script>
export default {
  name: "ProfileMenu"
}
</script>

<template lang="pug">
li.nav-item.dropdown
  a#languages-dropdown.nav-icon.dropdown-toggle(href="#" data-bs-toggle="dropdown")
    .position-relative
      i.fas.fa-globe-europe
      span.indicator en
  .dropdown-menu.dropdown-menu-end
    a.dropdown-item(href="#")
      i.fas.fa-caret-right
      |  Türkçe
    a.dropdown-item(href="#")
      i.fas.fa-caret-right
      |  English
</template>
Char
