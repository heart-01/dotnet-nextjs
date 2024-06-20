import colors from "tailwindcss/colors";
import type { Config } from "tailwindcss";
const config: Config = {
  content: ["./src/**/*.{scss,js,ts,jsx,tsx}", "./node_modules/preline/preline.js"],
  darkMode: ["class"],
  theme: {
    container: {
      center: true,
      padding: {
        DEFAULT: "1rem",
        sm: "1rem",
        md: "1rem",
        lg: "1rem",
        xl: "3rem",
        "2xl": "14rem",
      },
    },
    extend: {
      colors: {
        primary: colors.blue[600],
      },
    },
  },
  plugins: [require("preline/plugin")],
};
export default config;
