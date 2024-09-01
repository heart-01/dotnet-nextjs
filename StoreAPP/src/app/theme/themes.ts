"use client";

import { Roboto } from "next/font/google";
import { createTheme } from "@mui/material/styles";
import { Colors } from "./Colors";
import { typography } from "./Typography";
import { shadows } from "./Shadows";

const roboto = Roboto({
  weight: ["300", "400", "500", "700"],
  subsets: ["latin"],
  display: "swap",
});

const Themes = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: Colors.primary,
      light: Colors.primary,
      dark: Colors.primary,
    },
    secondary: {
      main: Colors.secondary,
      light: Colors.secondary,
      dark: Colors.secondary,
    },
  },
  typography,
  shadows,
});

export default Themes;
