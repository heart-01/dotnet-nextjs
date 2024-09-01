import { Box, Container, Typography } from "@mui/material";
import React from "react";

type Props = {};

export default function BlogPage({}: Props) {
  return (
    <>
      <Box display="flex" alignItems="center" justifyContent="center" height="65vh" flexDirection="column">
        <Container maxWidth="lg">
          <Typography variant="h1">Blog Page</Typography>
          <Typography variant="h2">This is the blog page</Typography>
          <Typography variant="body1" my={2}>
            Et aliquip sint nostrud esse nostrud non sint. Et duis quis ut do laboris fugiat do. Ex occaecat sit adipisicing aliquip dolore proident deserunt nisi excepteur. Incididunt tempor duis dolore amet mollit exercitation reprehenderit aliqua consectetur et eu sint deserunt officia. Aute sit ex enim ea deserunt reprehenderit. Veniam aute commodo elit consequat laboris deserunt consectetur et deserunt fugiat sunt. Non pariatur adipisicing deserunt proident Lorem exercitation occaecat id aute cupidatat esse laborum est.
          </Typography>
        </Container>
      </Box>
    </>
  );
}
