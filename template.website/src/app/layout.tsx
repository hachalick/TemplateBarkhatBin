import type { Metadata, Viewport } from "next";
import "./globals.css";
//import localFont from "next/font/local";


//const croissant = localFont({
//    variable: "--font-croissant",
//    src: [
//        {
//            path: "../Assets/font/croissant/woff2/Croissant-Black.woff2",
//            weight: "600",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff/Croissant-Black.woff",
//            weight: "600",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff2/Croissant-Bold.woff2",
//            weight: "500",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff/Croissant-Bold.woff",
//            weight: "500",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff2/Croissant-Medium.woff2",
//            weight: "300",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff/Croissant-Medium.woff",
//            weight: "300",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff2/Croissant-Light.woff2",
//            weight: "200",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff/Croissant-Light.woff",
//            weight: "200",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff2/Croissant-Hairline.woff2",
//            weight: "100",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/croissant/woff/Croissant-Hairline.woff",
//            weight: "100",
//            style: "normal",
//        },
//    ],
//});

//const iranyekan = localFont({
//    variable: "--font-iranyekan",
//    src: [
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-Black.woff2",
//            weight: "900",
//            style: "black",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-Bold.woff2",
//            weight: "bold",
//            style: "bold",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-DemiBold.woff2",
//            weight: "600",
//            style: "demibold",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-ExtraBlack.woff2",
//            weight: "950",
//            style: "extra-black",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-ExtraBold.woff2",
//            weight: "800",
//            style: "extra-bold",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-Heavy.woff2",
//            weight: "1000",
//            style: "heavy",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-Light.woff2",
//            weight: "300",
//            style: "light",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-Medium.woff2",
//            weight: "500",
//            style: "medium",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-Regular.woff2",
//            weight: "normal",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-Thin.woff2",
//            weight: "100",
//            style: "thin",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff2/IRANYekanX-UltraLight.woff2",
//            weight: "200",
//            style: "ultralight",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-Black.woff",
//            weight: "900",
//            style: "black",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-Bold.woff",
//            weight: "bold",
//            style: "bold",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-DemiBold.woff",
//            weight: "600",
//            style: "demibold",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-ExtraBlack.woff",
//            weight: "950",
//            style: "extra-black",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-ExtraBold.woff",
//            weight: "800",
//            style: "extra-bold",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-Heavy.woff",
//            weight: "1000",
//            style: "heavy",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-Light.woff",
//            weight: "300",
//            style: "light",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-Medium.woff",
//            weight: "500",
//            style: "medium",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-Regular.woff",
//            weight: "normal",
//            style: "normal",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-Thin.woff",
//            weight: "100",
//            style: "thin",
//        },
//        {
//            path: "../Assets/font/iranyekan/woff/IRANYekanX-UltraLight.woff",
//            weight: "200",
//            style: "ultralight",
//        },
//    ],
//});

export const metadata: Metadata = {
    title: "choconan | خانه کافه شوکونان",
    description:
        "شوکونان یک کافه واقع در سهرودی است که محصولات خود را بصورت بیرون بر یا سرو داخل کافه ارائه میدهد",
    manifest: "/manifest.json",
    appleWebApp: {
        capable: true,
        statusBarStyle: "default",
        title: "کافه شوکونان",
    },
    formatDetection: {
        telephone: false,
    },
    openGraph: {
        type: "website",
        siteName: "کافه شوکونان",
        title: "choconan | خانه کافه شوکونان",
        description:
            "شوکونان یک کافه واقع در سهرودی است که محصولات خود را بصورت بیرون بر یا سرو داخل کافه ارائه میدهد",
    },
    twitter: {
        card: "summary",
        title: {
            default: "choconan | خانه کافه شوکونان",
            template: "کافه شوکونان",
        },
        description:
            "شوکونان یک کافه واقع در سهرودی است که محصولات خود را بصورت بیرون بر یا سرو داخل کافه ارائه میدهد",
    },
};

export const viewport: Viewport = {
    themeColor: "#65449C",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en" dir="rtl">
            <body>
                {children}
            </body>
        {/*    <body className={`${croissant.variable} ${iranyekan.variable} relative`}>*/}
        {/*        {children}*/}
        {/*    </body>*/}
        </html>
    );
}
