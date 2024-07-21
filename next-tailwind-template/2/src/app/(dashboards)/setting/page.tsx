import { Metadata } from 'next'
import Footer4 from '@/components/common/Footer4'
import ScrollToTop from '@/components/ScrollToTop'
import AccountSetting from './AccountSetting'

export const metadata: Metadata = {
  title: 'Setting',
}
const Setting = () => {
  return (
    <>
      <AccountSetting />
      <Footer4 />
    </>
  )
}

export default Setting
