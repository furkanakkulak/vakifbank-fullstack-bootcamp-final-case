/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    "./src/**/*.{html,ts}",
    "./node_modules/flowbite/**/*.js" // add this line
  ],
  theme: {
    extend: {
      colors:{
        'primary': {
          '50': '#fffeeb',
          '100': '#fff8c7',
          '200': '#fff385',
          '300': '#ffe747',
          '400': '#ffd51a',
          '500': '#ffb300',
          '600': '#e08700',
          '700': '#bb5e02',
          '800': '#964a08',
          '900': '#7a3b0b',
          '950': '#471e00',
        },
      },
      fontFamily: {
        'poppins':"Poppins"
      }
    },

  },
  plugins: [
    require('flowbite/plugin') // add this line
  ],
}
