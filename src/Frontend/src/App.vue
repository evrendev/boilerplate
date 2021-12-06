<template lang="pug">
  #app
    component(:is="this.$route.meta.layout || 'div'")
      router-view
</template>
