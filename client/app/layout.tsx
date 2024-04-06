import { Metadata } from 'next';
import './globals.css';
import Header from './header/Header';
import ToastrProvider from './providers/ToastrProvider';

export const metadata: Metadata = {
  title: 'Auctioneer',
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang='en'>
      <body>
        <ToastrProvider />
        <Header />
        <main className='container mx-auto px-5 pt-10'>{children}</main>
      </body>
    </html>
  );
}
