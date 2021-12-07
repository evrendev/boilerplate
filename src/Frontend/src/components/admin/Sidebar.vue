<script>
export default {
  name: "Sidebar",
  data() {
    return {
      appName: process.env.VUE_APP_NAME
    }
  }
}
</script>

<template lang="pug">
  nav#sidebar.sidebar
    .sidebar-content.js-simplebar
      a.sidebar-brand(href="#")
        span.align-middle {{ appName }}
      ul.sidebar-nav
        li.sidebar-header
          | Pages
        li.sidebar-item.active
          router-link.sidebar-link(:to="{ name: 'admin.dashboard' }")
            i.fas.fa-tachometer-alt.align-middle
            span.align-middle Dashboard
        li.sidebar-item
          a.sidebar-link.collapsed(data-bs-target="#auth" data-bs-toggle="collapse")
            i.fas.fa-bars.align-middle
            span.align-middle Menu Example
          ul#auth.sidebar-dropdown.list-unstyled.collapse(data-bs-parent="#sidebar")
            li.sidebar-item
              a.sidebar-link(href="#") Menu Example
            li.sidebar-item
              a.sidebar-link(href="#") Menu Example
        li.sidebar-item
          a.sidebar-link(href="#")
            i.fas.fa-bars.align-middle
            span.align-middle Page Example
        li.sidebar-item
          a.sidebar-link(href="#")
            i.fas.fa-bars.align-middle
            span.align-middle Menu Example
        li.sidebar-item
          a.sidebar-link(href="#")
            i.fas.fa-bars.align-middle
            span.align-middle Menu Example
        li.sidebar-item
          a.sidebar-link(href="#")
            i.fas.fa-bars.align-middle
            span.align-middle Menu Example
</template>
