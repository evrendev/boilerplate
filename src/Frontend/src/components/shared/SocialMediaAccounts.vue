<script>
import Facebook from "@/assets/icons/Facebook.vue"
import Twitter from "@/assets/icons/Twitter.vue"
import Instagram from "@/assets/icons/Instagram.vue"
import Youtube from "@/assets/icons/Youtube.vue"

export default {
  name: "SocialMediaAccounts",
  components: {
    Facebook,
    Twitter,
    Instagram,
    Youtube
  }
}
</script>

<template lang="pug">
  .social-media-accounts
    a(href="https://www.facebook.com/Goztepe/" 
      target="_blank" 
      title="Facebook")
      span.visually-hidden Facebook'da takip edin
      Facebook
    a(href="https://twitter.com/Goztepe" 
      target="_blank" 
      title="Twitter")
      span.visually-hidden Twitter'da takip edin
      Twitter
    a(href="https://instagram.com/Goztepe" 
      target="_blank" 
      title="Instagram")
      span.visually-hidden Instagram'da takip edin
      Instagram
    a(href="https://youtube.com/Goztepe" 
      target="_blank" 
      title="Youtube")
      span.visually-hidden Youtube'da takip edin
      Youtube
</template>

<style lang="postcss" scoped>
.social-media-accounts {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 125px;
  height: 100%;
  margin-right: var(--default-space);

  a {
    span {
      &.visually-hidden {
        border: 0;
        clip: rect(0 0 0 0);
        height: 1px;
        margin: -1px;
        overflow: hidden;
        padding: 0;
        position: absolute;
        visibility: hidden;
      }
    }

    svg {
      width: 1.25em;
      height: 1.25em;
      fill: rgba(var(--white), 0.6);
      vertical-align: middle;
    }

    &:hover,
    &:active {
      svg {
        fill: rgb(var(--white));
      }
    }
  }
}
</style>
