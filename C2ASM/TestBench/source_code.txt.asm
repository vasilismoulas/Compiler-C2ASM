.model medium 
.stack 100h 
.data 
.code 
foo PROC
	
	push ebp
	mov ebp,esp
	push eax
	push ebx
	push ecx
	push edx
	push esi
	push edi
	
	;Arguments:
	
	;Function body:
	
fooEND:
	pop edi
	pop esi
	pop eax
	pop edx
	pop ecx
	pop ebx
	mov esp,ebp
	pop ebp
	ret
	
fooENDP

