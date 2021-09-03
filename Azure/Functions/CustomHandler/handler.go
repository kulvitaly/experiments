package main

import (
	"fmt"
	"log"
	"net/http"
	"os"
)

func helloHandler(w http.ResponseWriter, r *http.Request) {
	message := "Pass a name in the query string.\n"
	name := r.URL.Query().Get("name")
	if name != "" {
		message = fmt.Sprintf("Hello, %s", name)
	}

	fmt.Fprint(w, message)
}

func main() {
	listenAddr := ":8081"
	if val, ok := os.LookupEnv("FUNCTION_CUSTOMHANDLER_PORT"); ok {
		listenAddr = ":" + val
	}
	http.HandleFunc("/api/HttpTrigger1", helloHandler)
	log.Printf("https://127.0.0.1%s/", listenAddr)
	log.Fatal(http.ListenAndServe(listenAddr, nil))
}
