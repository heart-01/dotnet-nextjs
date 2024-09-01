import { Box, Container, Typography } from "@mui/material";
import React from "react";

type Props = {};

export default function AboutPage({}: Props) {
  return (
    <>
      <Box display="flex" alignItems="center" justifyContent="center" height="65vh" flexDirection="column">
        <Container maxWidth="lg">
          <Typography variant="h1">About Page</Typography>
          <Typography variant="h2">This is the about page</Typography>
          <Typography variant="body1" my={2}>
            Anim ipsum laboris in commodo et aliquip excepteur sunt ipsum. Ea cupidatat eu et eu mollit pariatur do magna veniam. Irure cillum velit adipisicing deserunt sunt exercitation eu ad
            eiusmod. Lorem est ipsum in adipisicing deserunt anim in non cillum exercitation minim ea. Ipsum labore eu excepteur ad quis. Ullamco labore fugiat magna ullamco. Eiusmod aute qui qui amet
            velit tempor.
          </Typography>
        </Container>
      </Box>
    </>
  );
}
