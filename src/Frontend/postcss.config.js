module.exports = {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
    "postcss-nested": {},
    "postcss-custom-media": {
      importFrom: [
        {
          customMedia: {
            "--sm": "(min-width: 640px)"
          }
        },
        {
          customMedia: {
            "--md": "(min-width: 780px)"
          }
        },
        {
          customMedia: {
            "--lg": "(min-width: 1024px)"
          }
        },
        {
          customMedia: {
            "--xl": "(min-width: 1280px)"
          }
        },
        {
          customMedia: {
            "--2xl": "(min-width: 1536px)"
          }
        }
      ]
    }
  }
}
