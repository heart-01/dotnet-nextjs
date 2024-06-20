"use client";

import { Roboto } from "next/font/google";
import { createTheme } from "@mui/material/styles";
import { Colors } from "./colors";

import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";

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
    },
    secondary: {
      main: Colors.secondary,
    },
  },
  typography: {
    fontFamily: roboto.style.fontFamily,
  },
});

export default Themes;
