import { Breadcrumb, Layout, Menu } from 'antd';
import React from 'react';
const { Header, Content, Footer } = Layout;

const BaseLayout = ({children}) => (
  <Layout style={{minHeight:'100vh'}}>
    <Header
      style={{
        position: 'fixed',
        zIndex: 1,
        width: '100%',
      }}
    >
      <div className="logo"/>      
    </Header>
    <Content
      className="site-layout"
      style={{        
        padding: '0 50px',
        marginTop: 64,
      }}
    >      
      <div
        className="site-layout-background"
        style={{
          padding: 24,
          minHeight:'100%',
        }}
      >
        {children}
      </div>
    </Content>
    <Footer
      style={{
        textAlign: 'center',
      }}
    >
      Challenge N5 ©2022 Created by Williams Galeano
    </Footer>
  </Layout>
);

export default BaseLayout;