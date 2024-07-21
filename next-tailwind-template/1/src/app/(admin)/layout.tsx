const FrontLayout = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  return (
    <>
      <div>{children}</div>
    </>
  );
};

export default FrontLayout;
