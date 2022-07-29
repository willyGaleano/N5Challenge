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
      <div className="logo" />
      <Menu
        theme="dark"
        mode="horizontal"
        defaultSelectedKeys={['1']}
        items={new Array(1).fill(null).map((_, index) => ({
          key: String(index + 1),
          label: "CHALLENGE N5",
        }))}
      />
    </Header>
    <Content
      className="site-layout"
      style={{        
        padding: '0 50px',
        marginTop: 64,
      }}
    >
      <Breadcrumb
        style={{
          margin: '16px 0',
        }}
      >
        <Breadcrumb.Item>N5</Breadcrumb.Item>
        <Breadcrumb.Item>Challenge</Breadcrumb.Item>
        <Breadcrumb.Item>Permissions</Breadcrumb.Item>
      </Breadcrumb>
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