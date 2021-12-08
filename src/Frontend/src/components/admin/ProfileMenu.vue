<script>
import { mapState, mapActions } from "vuex"

export default {
  name: "ProfileMenu",
  computed: {
    ...mapState("account", ["user"])
  },
  methods: {
    ...mapActions("account", ["logout"]),
    exit() {
      this.logout()
      this.$router.push("/")
    }
  }
}
</script>

<template lang="pug">
li.nav-item.dropdown
    a.nav-icon.dropdown-toggle.d-inline-block.d-sm-none(href="#" 
        data-bs-toggle="dropdown")
        i.far.fa-user-circle
    a.nav-link.dropdown-toggle.d-none.d-sm-inline-block(href="#" 
        data-bs-toggle="dropdown")
        img.avatar.img-fluid.rounded.me-1(src="https://avatars.dicebear.com/api/male/john.svg?mood[]=happy")
        span.text-dark Charles Hall
    .dropdown-menu.dropdown-menu-end
        router-link.dropdown-item(to="/admin/user")
            i.far.fa-user
            |  Profile
        a.dropdown-item(href="https://faq.evren.dev/"
            target="_blank")
            i.fas.fa-question
            |  Help Center
        .dropdown-divider
        a.dropdown-item(@click="exit") 
            i.fas.fa-sign-out-alt
            |  Logout
</template>
