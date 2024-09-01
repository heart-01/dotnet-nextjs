"use client";

import { MenuOutlined as MenuIcon } from "@mui/icons-material";
import { AppBar, Box, Button, Container, IconButton, styled, Theme, Toolbar, useMediaQuery } from "@mui/material";
import Image from "next/image";
import Link from "next/link";
import React from "react";

type Props = {};

const Header = (props: Props) => {
  // AppBar Stylying
  const AppBarStyled = styled(AppBar)(({ theme }) => ({
    justifyContent: "center",
    [theme.breakpoints.up("lg")]: {
      minHeight: "80px",
    },
    backgroundColor: theme.palette.background.default,
  }));

  // Toolbar Styling
  const ToolbarStyled = styled(Toolbar)(({ theme }) => ({
    width: "100%",
    paddingLeft: "0 !important",
    paddingRight: "0 !important",
    color: theme.palette.text.secondary,
  }));

  // Screen Size
  const lgUp = useMediaQuery((theme: Theme) => theme.breakpoints.up("lg"));
  const lgDown = useMediaQuery((theme: Theme) => theme.breakpoints.down("lg"));

  // Button Styling
  const ButtonStyled = styled(Button)(({ theme }) => ({
    fontSize: "16px",
    color: theme.palette.text.secondary,
  }));

  return (
    <>
      <AppBarStyled position="sticky" elevation={8}>
        <Container maxWidth="lg">
          <ToolbarStyled>
            <Image src={"/images/logos/dark-logo.svg"} alt="logo" width={174} height={40} />
            <Box flexGrow={1} />
            {lgDown && (
              <IconButton edge="start" color="inherit" aria-label="menu">
                <MenuIcon />
              </IconButton>
            )}
            {lgUp && (
              <>
                <ButtonStyled color="inherit" variant="text" LinkComponent={Link} href="/">
                  Home
                </ButtonStyled>
                <ButtonStyled color="inherit" variant="text" LinkComponent={Link} href="/about">
                  About
                </ButtonStyled>
                <ButtonStyled color="inherit" variant="text" LinkComponent={Link} href="/blog">
                  Blog
                </ButtonStyled>
                <ButtonStyled color="inherit" variant="text" LinkComponent={Link} href="/contact">
                  Contact
                </ButtonStyled>
                <Button color="primary" variant="contained" LinkComponent={Link} href="/login">
                  Login
                </Button>
              </>
            )}
          </ToolbarStyled>
        </Container>
      </AppBarStyled>
    </>
  );
};

export default Header;
