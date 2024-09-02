"use client";

import { styled } from "@mui/material";
import Sidebar from "@/app/components/back/sidebar/Sidebar";

const MainWrapper = styled("div")(() => ({
  display: "flex",
  minHeight: "100vh",
  width: "100%",
}));

const BackLayout = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  return (
    <MainWrapper>
      <Sidebar />
    </MainWrapper>
  );
};

export default BackLayout;
