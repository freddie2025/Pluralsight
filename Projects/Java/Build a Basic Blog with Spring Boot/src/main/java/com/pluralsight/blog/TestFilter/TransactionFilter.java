package com.pluralsight.blog.TestFilter;

import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;
import org.springframework.util.ErrorHandler;
import org.springframework.web.context.support.WebApplicationContextUtils;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import java.io.IOException;

@Component
@Order(1)
public class TransactionFilter implements Filter {

//    ErrorHandler errorHandler;
//
//    @Override
//    public void init(FilterConfig filterConfig) throws ServletException {
//        errorHandler = (ErrorHandler) WebApplicationContextUtils
//                .getRequiredWebApplicationContext(filterConfig.getServletContext())
//                .getBean("defaultErrorHandler");
//    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain) throws IOException, ServletException {
        chain.doFilter(request, response);
    }

    // other methods
}
