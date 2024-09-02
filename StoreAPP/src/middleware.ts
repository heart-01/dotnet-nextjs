import { NextRequest, NextResponse } from "next/server";

export async function middleware(request: NextRequest) {
  try {
    const parsedUrl = new URL(request.url);
    const pathname = parsedUrl.pathname.replace(/^(\/)/, "");

    // list public routes
    const publicRoutes = ["login", "register", "forgotpass", "twostep"];
    const token = true;

    if (token && publicRoutes.includes(pathname)) {
      return NextResponse.redirect(new URL("/backend/dashboard", request.nextUrl));
    }

    if (!token && !publicRoutes.includes(pathname)) {
      return NextResponse.redirect(new URL("/login", request.nextUrl));
    }

    return NextResponse.next();
  } catch (error) {
    console.error(error);
    return NextResponse.error();
  }
}

export const config = {
  matcher: ["/login", "/register", "/forgotpass", "/twostep", "/backend/:path*"],
};
