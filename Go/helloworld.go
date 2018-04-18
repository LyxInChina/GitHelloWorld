
package main//定义包名称 main

import "fmt" //导入包 fmt
//定义函数 main
func main(){	
	//调用fmt包中的方法Printf
	fmt.Printf("hello world! \n")
}
//先调用init 函数 然后再调用main函数
func init(){
	fmt.Printf("hello ! \n")
}

