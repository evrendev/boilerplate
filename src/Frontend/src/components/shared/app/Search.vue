<template lang="pug">
.search
    label.search-toggle(for="search-toggle")
        input#search-toggle.search-toggle-box(type="checkbox" name="search-toggle")
        SearchIcon
        CloseIcon
</template>

<script>
import SearchIcon from "@/assets/icons/Search.vue"
import CloseIcon from "@/assets/icons/Close.vue"
export default {
  name: "Search",
  components: {
    SearchIcon,
    CloseIcon
  }
}
</script>

<style lang="postcss" scoped>
.search {
  width: 30px;
  height: 30px;
  display: flex;
  align-items: center;

  label {
    &.search-toggle {
      cursor: pointer;
      height: 30px;

      svg {
        fill: rgb(var(--white));
        width: 30px;
        height: 30px;
        transform-origin: center;
        transition: all 0.125s ease-in-out;

        &.icon-close {
          visibility: hidden;
          opacity: 0;
          transform: scale(0.5);
        }
      }

      input[type="checkbox"] {
        display: none;

        &:checked {
          ~ svg {
            &.icon-search {
              visibility: hidden;
              opacity: 0;
              transform: scale(0.5);
            }
          }

          ~ svg {
            &.icon-close {
              visibility: visible;
              opacity: 1;
              transform: scale(1);
            }
          }
        }
      }
    }
  }
}
</style>
