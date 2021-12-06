<template lang="pug">
  .menu-button
    input(type="checkbox" id="hamburger-menu-button")
    label(for="hamburger-menu-button")
      span.bar.top
      span.bar.middle
      span.bar.bottom       
    aside
      .menu-content
        h1 Menü
</template>

<script>
export default {
  name: "Menu"
}
</script>

<style lang="postcss" scoped>
.menu-button {
  margin-left: var(--default-space);

  input {
    &[type="checkbox"] {
      display: none;

      ~ label {
        display: inline-block;
        background-color: transparent;
        cursor: pointer;
        z-index: 3;

        span {
          &.bar {
            display: block;
            background-color: rgb(var(--white));
            width: 30px;
            height: 2px;
            margin: 8px auto;
            border-radius: 5px;
            transition: background-color 0.4s ease-in, transform 0.4s ease-in, width 0.4s ease-in;

            @media (--sm) {
              width: 30px;
              height: 3px;
              margin: 10px auto;
            }

            &.middle {
              width: 20px;
              margin-left: 0;
            }

            &.bottom {
              width: 25px;
              margin-left: 0;
            }
          }
        }
      }

      ~ aside {
        align-items: center;
        justify-content: center;
        background-color: rgb(var(--yellow));
        background-size: cover;
        background-repeat: no-repeat;
        background-position: right center;

        position: absolute;
        left: 0;
        width: 100vw;
        top: 60px;
        height: calc(100vh - 60px);

        -webkit-transform: translateX(-100%);
        -moz-transform: translateX(-100%);
        -ms-transform: translateX(-100%);
        -o-transform: translateX(-100%);
        transform: translateX(-100%);
        transition: transform 0.4s ease-in-out;

        @media (--sm) {
          top: 121px;
          height: calc(100vh - 121px);
        }

        .menu-content {
          color: rgb(var(--black));
        }
      }

      &:checked ~ label {
        span {
          &.bar {
            background-color: rgb(var(--white));

            &.top {
              -webkit-transform: translateY(8px) rotateZ(45deg);
              -moz-transform: translateY(8px) rotateZ(45deg);
              -ms-transform: translateY(8px) rotateZ(45deg);
              -o-transform: translateY(8px) rotateZ(45deg);
              transform: translateY(8px) rotateZ(45deg);

              @media (--sm) {
                -webkit-transform: translateY(10px) rotateZ(45deg);
                -moz-transform: translateY(10px) rotateZ(45deg);
                -ms-transform: translateY(10px) rotateZ(45deg);
                -o-transform: translateY(10px) rotateZ(45deg);
                transform: translateY(10px) rotateZ(45deg);
              }
            }

            &.middle {
              width: 0;
              margin: 0 auto;
            }

            &.bottom {
              width: 30px;

              -webkit-transform: translateY(-13px) rotateZ(-45deg);
              -moz-transform: translateY(-13px) rotateZ(-45deg);
              -ms-transform: translateY(-13px) rotateZ(-45deg);
              -o-transform: translateY(-13px)) rotateZ(-45deg);
              transform: translateY(-13px) rotateZ(-45deg);

              @media (--sm) {
                -webkit-transform: translateY(-15px) rotateZ(-45deg);
                -moz-transform: translateY(-15px) rotateZ(-45deg);
                -ms-transform: translateY(-15px) rotateZ(-45deg);
                -o-transform: translateY(-15px)) rotateZ(-45deg);
                transform: translateY(-15px) rotateZ(-45deg);
              }
            }
          }
        }
      }

      &:checked ~ aside {
        display: flex;
        transform: translateY(0%);
      }
    }
  }
}
</style>
